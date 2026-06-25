using System;
using System.Collections.Generic;
using System.Text;

using SupermarketManagementSystem.ShopData;

namespace SupermarketManagementSystem.ShopDataStructures
{
    public class SupplierRecordArray
    {
        private Supplier[] suppliers;
        private int count;

        public SupplierRecordArray()
        {
            suppliers = new Supplier[5];
            count = 0;
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public void AddSupplier(Supplier supplier)
        {
            if (count == suppliers.Length)
            {
                ResizeArray();
            }

            suppliers[count] = supplier;
            count++;
        }

        public Supplier? GetSupplierAt(int index)
        {
            if (index < 0 || index >= count)
            {
                return null;
            }

            return suppliers[index];
        }

        public Supplier? SearchBySupplierId(string supplierId)
        {
            for (int i = 0; i < count; i++)
            {
                if (suppliers[i].SupplierId == supplierId)
                {
                    return suppliers[i];
                }
            }

            return null;
        }

        public bool RemoveSupplier(string supplierId)
        {
            for (int i = 0; i < count; i++)
            {
                if (suppliers[i].SupplierId == supplierId)
                {
                    for (int j = i; j < count - 1; j++)
                    {
                        suppliers[j] = suppliers[j + 1];
                    }

                    count--;
                    return true;
                }
            }

            return false;
        }

        private void ResizeArray()
        {
            Supplier[] largerArray = new Supplier[suppliers.Length * 2];

            for (int i = 0; i < count; i++)
            {
                largerArray[i] = suppliers[i];
            }

            suppliers = largerArray;
        }
    }
}