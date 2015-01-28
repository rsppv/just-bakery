namespace JustBakery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sec : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "PersonID", "dbo.People");
            DropForeignKey("dbo.ProductAccountingLogs", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.DetailProductOperations", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductResidues", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Employees", "BakeryID", "dbo.Bakeries");
            DropForeignKey("dbo.Employees", "PersonID", "dbo.People");
            DropForeignKey("dbo.Employees", "PositionID", "dbo.Positions");
            DropForeignKey("dbo.ProductAccountingLogs", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.DetailRawOperations", "RawID", "dbo.Raws");
            DropForeignKey("dbo.RawResidues", "RawID", "dbo.Raws");
            DropForeignKey("dbo.RawResidues", "StockID", "dbo.Stocks");
            DropForeignKey("dbo.Ingridients", "RawTypeID", "dbo.RawTypes");
            DropForeignKey("dbo.Ingridients", "RecipeID", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Raws", "RawTypeID", "dbo.RawTypes");
            DropForeignKey("dbo.DetailRawOperations", "RawAccountingLog_LogRecordID", "dbo.RawAccountingLogs");
            DropForeignKey("dbo.RawAccountingLogs", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.ProductAccountingLogs", "OperationTypeID", "dbo.OperationTypes");
            DropForeignKey("dbo.RawAccountingLogs", "OperationTypeID", "dbo.OperationTypes");
            DropForeignKey("dbo.RawAccountingLogs", "StockID", "dbo.Stocks");
            DropForeignKey("dbo.Suppliers", "Person_PersonID", "dbo.People");
            DropForeignKey("dbo.RawAccountingLogs", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.Stocks", "BakeryID", "dbo.Bakeries");
            DropForeignKey("dbo.ProductAccountingLogs", "StockID", "dbo.Stocks");
            DropForeignKey("dbo.ProductResidues", "StockID", "dbo.Stocks");
            DropForeignKey("dbo.Products", "ProductTypeID", "dbo.ProductTypes");
            DropForeignKey("dbo.DetailProductOperations", "ProductAccountingLog_LogRecordID", "dbo.ProductAccountingLogs");
            DropForeignKey("dbo.AspNetUsers", "Person_PersonID", "dbo.People");
            DropIndex("dbo.AspNetUsers", new[] { "Person_PersonID" });
            DropIndex("dbo.Customers", new[] { "PersonID" });
            DropIndex("dbo.ProductAccountingLogs", new[] { "OperationTypeID" });
            DropIndex("dbo.ProductAccountingLogs", new[] { "StockID" });
            DropIndex("dbo.ProductAccountingLogs", new[] { "CustomerID" });
            DropIndex("dbo.ProductAccountingLogs", new[] { "EmployeeID" });
            DropIndex("dbo.DetailProductOperations", new[] { "ProductID" });
            DropIndex("dbo.DetailProductOperations", new[] { "ProductAccountingLog_LogRecordID" });
            DropIndex("dbo.Products", new[] { "ProductTypeID" });
            DropIndex("dbo.ProductResidues", new[] { "StockID" });
            DropIndex("dbo.ProductResidues", new[] { "ProductID" });
            DropIndex("dbo.Stocks", new[] { "BakeryID" });
            DropIndex("dbo.Employees", new[] { "PersonID" });
            DropIndex("dbo.Employees", new[] { "PositionID" });
            DropIndex("dbo.Employees", new[] { "BakeryID" });
            DropIndex("dbo.RawAccountingLogs", new[] { "OperationTypeID" });
            DropIndex("dbo.RawAccountingLogs", new[] { "StockID" });
            DropIndex("dbo.RawAccountingLogs", new[] { "SupplierID" });
            DropIndex("dbo.RawAccountingLogs", new[] { "EmployeeID" });
            DropIndex("dbo.DetailRawOperations", new[] { "RawID" });
            DropIndex("dbo.DetailRawOperations", new[] { "RawAccountingLog_LogRecordID" });
            DropIndex("dbo.Raws", new[] { "RawTypeID" });
            DropIndex("dbo.RawResidues", new[] { "StockID" });
            DropIndex("dbo.RawResidues", new[] { "RawID" });
            DropIndex("dbo.Ingridients", new[] { "RecipeID" });
            DropIndex("dbo.Ingridients", new[] { "RawTypeID" });
            DropIndex("dbo.Recipes", new[] { "ProductID" });
            DropIndex("dbo.Suppliers", new[] { "Person_PersonID" });
            AddColumn("dbo.AspNetUsers", "PersonId", c => c.Guid(nullable: false));
            AddForeignKey("dbo.AspNetUsers", "PersonId", "Class.Личности", "ID_Личности");
            
        }
        
        public override void Down()
        {
           
            DropColumn("dbo.AspNetUsers", "PersonId");
            CreateIndex("dbo.Suppliers", "Person_PersonID");
            CreateIndex("dbo.Recipes", "ProductID");
            CreateIndex("dbo.Ingridients", "RawTypeID");
            CreateIndex("dbo.Ingridients", "RecipeID");
            CreateIndex("dbo.RawResidues", "RawID");
            CreateIndex("dbo.RawResidues", "StockID");
            CreateIndex("dbo.Raws", "RawTypeID");
            CreateIndex("dbo.DetailRawOperations", "RawAccountingLog_LogRecordID");
            CreateIndex("dbo.DetailRawOperations", "RawID");
            CreateIndex("dbo.RawAccountingLogs", "EmployeeID");
            CreateIndex("dbo.RawAccountingLogs", "SupplierID");
            CreateIndex("dbo.RawAccountingLogs", "StockID");
            CreateIndex("dbo.RawAccountingLogs", "OperationTypeID");
            CreateIndex("dbo.Employees", "BakeryID");
            CreateIndex("dbo.Employees", "PositionID");
            CreateIndex("dbo.Employees", "PersonID");
            CreateIndex("dbo.Stocks", "BakeryID");
            CreateIndex("dbo.ProductResidues", "ProductID");
            CreateIndex("dbo.ProductResidues", "StockID");
            CreateIndex("dbo.Products", "ProductTypeID");
            CreateIndex("dbo.DetailProductOperations", "ProductAccountingLog_LogRecordID");
            CreateIndex("dbo.DetailProductOperations", "ProductID");
            CreateIndex("dbo.ProductAccountingLogs", "EmployeeID");
            CreateIndex("dbo.ProductAccountingLogs", "CustomerID");
            CreateIndex("dbo.ProductAccountingLogs", "StockID");
            CreateIndex("dbo.ProductAccountingLogs", "OperationTypeID");
            CreateIndex("dbo.Customers", "PersonID");

   
            AddForeignKey("dbo.DetailProductOperations", "ProductAccountingLog_LogRecordID", "dbo.ProductAccountingLogs", "LogRecordID");
            AddForeignKey("dbo.Products", "ProductTypeID", "dbo.ProductTypes", "ProductTypeID", cascadeDelete: true);
            AddForeignKey("dbo.ProductResidues", "StockID", "dbo.Stocks", "StockID", cascadeDelete: true);
            AddForeignKey("dbo.ProductAccountingLogs", "StockID", "dbo.Stocks", "StockID", cascadeDelete: true);
            AddForeignKey("dbo.Stocks", "BakeryID", "dbo.Bakeries", "BakeryID");
            AddForeignKey("dbo.RawAccountingLogs", "SupplierID", "dbo.Suppliers", "SupplierID");
            AddForeignKey("dbo.Suppliers", "Person_PersonID", "dbo.People", "PersonID");
            AddForeignKey("dbo.RawAccountingLogs", "StockID", "dbo.Stocks", "StockID", cascadeDelete: true);
            AddForeignKey("dbo.RawAccountingLogs", "OperationTypeID", "dbo.OperationTypes", "OperationTypeID", cascadeDelete: true);
            AddForeignKey("dbo.ProductAccountingLogs", "OperationTypeID", "dbo.OperationTypes", "OperationTypeID", cascadeDelete: true);
            AddForeignKey("dbo.RawAccountingLogs", "EmployeeID", "dbo.Employees", "EmployeeID");
            AddForeignKey("dbo.DetailRawOperations", "RawAccountingLog_LogRecordID", "dbo.RawAccountingLogs", "LogRecordID");
            AddForeignKey("dbo.Raws", "RawTypeID", "dbo.RawTypes", "RawTypeID", cascadeDelete: true);
            AddForeignKey("dbo.Recipes", "ProductID", "dbo.Products", "ProductID", cascadeDelete: true);
            AddForeignKey("dbo.Ingridients", "RecipeID", "dbo.Recipes", "RecipeID", cascadeDelete: true);
            AddForeignKey("dbo.Ingridients", "RawTypeID", "dbo.RawTypes", "RawTypeID", cascadeDelete: true);
            AddForeignKey("dbo.RawResidues", "StockID", "dbo.Stocks", "StockID", cascadeDelete: true);
            AddForeignKey("dbo.RawResidues", "RawID", "dbo.Raws", "RawID", cascadeDelete: true);
            AddForeignKey("dbo.DetailRawOperations", "RawID", "dbo.Raws", "RawID", cascadeDelete: true);
            AddForeignKey("dbo.ProductAccountingLogs", "EmployeeID", "dbo.Employees", "EmployeeID");
            AddForeignKey("dbo.Employees", "PositionID", "dbo.Positions", "PositionID", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "PersonID", "dbo.People", "PersonID", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "BakeryID", "dbo.Bakeries", "BakeryID");
            AddForeignKey("dbo.ProductResidues", "ProductID", "dbo.Products", "ProductID", cascadeDelete: true);
            AddForeignKey("dbo.DetailProductOperations", "ProductID", "dbo.Products", "ProductID", cascadeDelete: true);
            AddForeignKey("dbo.ProductAccountingLogs", "CustomerID", "dbo.Customers", "CustomerID");
            AddForeignKey("dbo.Customers", "PersonID", "dbo.People", "PersonID", cascadeDelete: true);
        }
    }
}
