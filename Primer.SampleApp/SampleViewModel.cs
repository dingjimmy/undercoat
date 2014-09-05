﻿using Primer;
using System;
using System.Windows.Input;
using System.Linq;
using Primer.Messages;

namespace Primer.SampleApp
{

    public class SampleCustomerViewModel : ViewModel<CustomerFacade>
    {

        // Dependancies
        DataContext _Context;


        // Sub-ViewModel collections
        public ViewModelCollection<DetailViewModel> Details { get; set; }
        public ViewModelCollection MoreDetails { get; set; }



        // Lookups
        public Lookup<Supplier> AvailableSuppliers { get; set; }



        // Commands
        public Command Ok { get; set; }
        public Command Cancel { get; set; }





        public SampleCustomerViewModel(DataContext ctx)
        {

            // Set dependancies
            _Context = ctx;
            Channel = new MessagingChannel();
            //Validator = new CustomerValidator();


            // build queries
            var cusQuery = from c in _Context.Customers where c.ID == 1876309338 select c;
            var dtlQuery = from d in _Context.Details select d;
            var splQuery = from s in _Context.Suppliers select s;         


            Initialise(cusQuery, dtlQuery, splQuery);

        }


        protected override void Initialise(ViewModelInitialiser initialise, params object[] dataSources)
        {

            // Verify dependacies
            var customers = dataSources[0] as IQueryable<Customer>;
            var details = dataSources[1] as IQueryable<OrderDetail>;
            var suppliers = dataSources[2] as IQueryable<Supplier>;


            // Set the model
            Model = new CustomerFacade(customers.First(), this.Channel, new CustomerValidator());

           


            // Init a collection of ViewModels using a specific initialisation method.
            Details = initialise.Collection<DetailViewModel, OrderDetail>(details, (init, item, vm) =>
                {
                    vm.Model = new OrderDetailFacade(item, this.Channel); 
                });



            // Init collection of ViewModels using the ViewModel's default initialisation method.
            MoreDetails = initialise.Collection<DetailViewModel, OrderDetail>(details);

            

            // Init Lookups
            AvailableSuppliers = initialise.Lookup<Supplier>(suppliers, (supplier, item) =>
                {
                    item.Key = supplier.ID.ToString();
                    item.Description = String.Format("{0} - {1}", supplier.Name, supplier.Branch);
                    item.Entity = supplier;
                });



            // Init Commands
            this.Ok = new Command { Action = SaveThis, IsEnabled = true };
            this.Cancel = new Command { Action = CancelThis, IsEnabled = true };



            // Listen for FieldChanged messages
            Listen<PropertyChanged>(m =>
                {
                    System.Diagnostics.Debug.WriteLine("A 'PropertyChanged' message was broadcast by '{0}' at '{1}'. Property Name: {2}", m.Sender.GetType().ToString(), m.BroadcastOn, m.Name);
                });


            // Change a fields value; this will broadcast the PropertyChanged message which should cause the above listener method to be executed!
            Model.SetFirstName("Dicky", true, false);
            Model.SetFamilyName("Wee Wee", false, true);
            Details[0].Model.Description = "This field has changed!!!";


            //var nameOne = GetMethodName(() => this.TestSubRoutine());
            //var nameTwo = GetMethodName(() => this.TestSubRoutineWithParameter(true));

            //var nameThree = GetMethodName(() => this.TestFunction());
            //var nameFour = GetMethodName(() => this.TestFunctionWithParameter(true));
            //var nameFive = GetMethodName(() => this.TestFunctionWithTwoParameters(true, true));


            //HandleException(() => this.TestSubRoutine(), new ApplicationException("Test SubRoutine"));
            //HandleException(() => this.TestFunction(), new ApplicationException("Test Function"));

        }



        #region CommandMethods


        public void SaveThis()
        {


            Validate("ID", "Name", "EmailAddress");

            if (!InError("ID", "Name", "EmailAddress"))
            {
                // save me to a database!
            }

            Model.FirstName = "This is a test!";
        }



        public void CancelThis()
        {

        }


        #endregion


        public void DoSomethingSpecial()
        {
            AvailableSuppliers.ClearFilter();
            AvailableSuppliers.ApplyFilter((item) => item.Entity.ID <= 6);
            //AvailableSuppliers.FilterIn((item) => item.Entity > 3 && item.Key < 10);
        }


        public void TestSubRoutine() { }

        public void TestSubRoutineWithParameter(bool isTest) { }

        public bool TestFunction() { return true; }

        public bool TestFunctionWithParameter(bool isTest) { return isTest; }

        public bool TestFunctionWithTwoParameters(bool isTest, bool thisIsGay) { return isTest; }
    }

}
