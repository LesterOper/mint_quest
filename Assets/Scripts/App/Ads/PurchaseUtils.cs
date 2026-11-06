using System;
using System.Collections;
using System.Collections.Generic;
using Swimmer.Library.Common;
using UnityEngine;
using VoxelBusters.NativePlugins;

public class PurchaseUtils : MonoBehaviour
{
    public delegate void OperationCompletedHandler(string error);

    private BillingProduct[] _loadedProducts;

    private OperationCompletedHandler _operationCompletedHandler;

    //null for restore
    private string _processingSku;

    private void Awake()
    {
        Debug.Log("[PremPurchaseHelper] Awake: ");
    }

    private void OnEnable()
    {
        if (GetComponent<Context>().Redundant)
            return;
        Debug.Log("[PremPurchaseHelper] OnEnable: ");
        Billing.DidFinishRequestForBillingProductsEvent += OnDidFinishProductsRequest;
        Billing.DidFinishProductPurchaseEvent += OnDidFinishTransaction;
        Billing.DidFinishRestoringPurchasesEvent += OnDidFinishRestoringPurchases;

        RestoreCompletedTransactions(null);
    }

    private void OnDisable()
    {
        if (GetComponent<Context>().Redundant)
            return;
            
        Debug.Log("[PremPurchaseHelper] OnDisable: ");
        Billing.DidFinishRequestForBillingProductsEvent -= OnDidFinishProductsRequest;
        Billing.DidFinishProductPurchaseEvent -= OnDidFinishTransaction;
        Billing.DidFinishRestoringPurchasesEvent -= OnDidFinishRestoringPurchases;
    }

    /** null for restore purchases */
    private void LoadProducts()
    {
        if (NPBinding.Billing.IsAvailable())
        {
            Debug.Log("[PremPurchaseHelper] LoadProducts: c=" + NPSettings.Billing.Products.Length);
            NPBinding.Billing.RequestForBillingProducts(NPSettings.Billing.Products);
        }
    }

    public void RestoreCompletedTransactions(OperationCompletedHandler completeHandler)
    {
        Debug.Log("[PremPurchaseHelper] RestoreCompletedTransactions: ");
        if (!NPBinding.Billing.IsAvailable())
        {
            completeHandler("Billing is not available.");
            return;
        }

        _operationCompletedHandler = completeHandler;

        if (_loadedProducts == null)
        {
            LoadProducts();
            return;
        }

        NPBinding.Billing.RestorePurchases();
    }

    public void MakePurchase(string sku, OperationCompletedHandler completeHandler)
    {
        Debug.Log("[PremPurchaseHelper] MakePurchase: " + sku);
        if (!NPBinding.Billing.IsAvailable())
        {
            completeHandler("Billing is not available."); 
            return;
        }

        _operationCompletedHandler = completeHandler;

        if (_loadedProducts == null)
        {
            _processingSku = sku;
            LoadProducts();
            return;
        }

        var product = _loadedProducts[0];

        if (product == null) throw new SystemException("No BillingProduct found for Purchase '" + sku + "'.");
        Debug.Log("[PremPurchaseHelper] MakePurchase: " + product);
        if (!NPBinding.Billing.IsProductPurchased(product))
            NPBinding.Billing.BuyProduct(product);
        else
            completeHandler("Already purchased.");
    }

    private void OnDidFinishProductsRequest(BillingProduct[] regProducts, string error)
    {
        Debug.Log("[PremPurchaseHelper] OnDidFinishProductsRequest: success=" + (error == null) + "; error='" +
                  error
                  + "'; count=" + (regProducts != null ? regProducts.Length : 0));
        if (error != null || regProducts == null)
        {
            _processingSku = null;
            _operationCompletedHandler?.Invoke(error);
        }
        else
        {
            _loadedProducts = regProducts;

            if (_processingSku == null)
            {
                RestoreCompletedTransactions(_operationCompletedHandler);
            }
            else
            {
                MakePurchase(_processingSku, _operationCompletedHandler);
                _processingSku = null;
            }
        }
    }

    private void OnDidFinishTransaction(BillingTransaction transaction)
    {
        Debug.Log("[PremPurchaseHelper] OnDidFinishTransaction: " + transaction);
        if (transaction != null)
        {
            if (transaction.VerificationState == eBillingTransactionVerificationState.NOT_CHECKED)
            {
                transaction.OnCustomVerificationFinished(eBillingTransactionVerificationState.SUCCESS);
            }
            else if (transaction.TransactionState == eBillingTransactionState.PURCHASED)
            {
                _operationCompletedHandler?.Invoke(transaction.Error);
            }
        }
    }

    private void OnDidFinishRestoringPurchases(BillingTransaction[] transactions, string error)
    {
        Debug.Log("[PremPurchaseHelper] OnDidFinishRestoringPurchases: " +
                  "count = " + transactions.Length + "; sucess = " + (error == null) + "; " + error);
        if (error == null)
        {
            var restoredTransactionIds = new List<string>();

            foreach (var transaction in transactions)
                if (!restoredTransactionIds.Contains(transaction.ProductIdentifier))
                {
                    Debug.Log("[PremPurchaseHelper] OnDidFinishRestoringPurchases transaction: " + transaction);
                    restoredTransactionIds.Add(transaction.ProductIdentifier);
                }
        }

        _operationCompletedHandler?.Invoke(error);
    }
    
}