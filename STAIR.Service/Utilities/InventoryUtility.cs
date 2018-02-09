using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;
using STAIR.LoggerService;
using STAIR.Model.Models;
using STAIR.Service;

namespace STAIR.Service
{
    public class InventoryUtility
    {
        public readonly IItemBalanceService itemBalanceService;
        public readonly IInventoryHistoryService inventoryHistoryService;
        private readonly LoggingService logger = new LoggingService(typeof(ItemIssueService));
        public InventoryUtility(IInventoryHistoryService inventoryHistoryService, IItemBalanceService itemBalanceService)
        {
            this.itemBalanceService = itemBalanceService;
            this.inventoryHistoryService = inventoryHistoryService;
        }

        
        public bool SaveInventoryHistory(int? aircraftComponentId, string referenceId, string partNo, double? quantity, int? rackRowBinIn, int entryStatus, int useIn, double? spareQty, string serial, string narration)
        {
            InventoryHistory inventoryHistory = new InventoryHistory();
            inventoryHistory.Id = Guid.NewGuid();
            inventoryHistory.AircraftComponentId = aircraftComponentId;
            inventoryHistory.ReferenceId = referenceId;
            inventoryHistory.PartNo = partNo;
            inventoryHistory.Quantity = quantity;
            inventoryHistory.RecordDate = DateTime.UtcNow;
            inventoryHistory.EntryStatus = entryStatus;
            inventoryHistory.RackRowBinId = rackRowBinIn;
            inventoryHistory.UseIn = useIn;

            return this.inventoryHistoryService.CreateInventoryHistory(inventoryHistory);
        }

        public bool CreatItemBalance(ItemBalance itemBalance)
        {
            if (itemBalance != null)
            {
                return this.itemBalanceService.CreateItemBalance(itemBalance);
            }
            return false;
        }

        public bool UpdateItmeBalance(ItemBalance itemBalance, double? quantity, double? spareQuantity, bool isPlSTAIRlance)
        {
            if (itemBalance != null)
            {
                if (quantity == null) quantity = 0;
                if (spareQuantity == null) spareQuantity = 0;

                if (isPlSTAIRlance)
                {
                    itemBalance.Quantity += quantity;
                    itemBalance.SpareFloatQty += spareQuantity;
                }
                else if (!isPlSTAIRlance)
                {
                    itemBalance.Quantity -= quantity;
                    itemBalance.SpareFloatQty -= spareQuantity;
                }

                if (itemBalance.Quantity < 0)
                {
                    logger.Warn("Item Balance gone negative!");
                }
                else
                {
                    return this.itemBalanceService.UpdateItemBalance(itemBalance);
                }
            }
            return false;
        }

        public bool CheckBalanceQuantityIsNegative(double? balanceQty, double? QtyToUpdate, bool isPlSTAIRlance)
        {
            if (balanceQty == null) balanceQty = 0;
            if (QtyToUpdate == null) QtyToUpdate = 0;

            if (isPlSTAIRlance)
                balanceQty += QtyToUpdate;
            else if (!isPlSTAIRlance)
                balanceQty -= QtyToUpdate;

            if (balanceQty < 0)
            {
                logger.Warn("Item Balance gone negative!");
                return true;
            }
            return false;
        }
    }
}