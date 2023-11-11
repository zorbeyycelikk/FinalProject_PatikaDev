import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/services/customer.service';
import { StorageService } from 'src/app/services/storage.service';
import { Customer } from './customer';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {

  customer? : Customer[];
  userRole?: string;
  userId?:string
  filterText = "";
  selectedCustomer: Customer = {};

  constructor(
    private storage:StorageService,
    private customerService:CustomerService,
    ){}

    ngOnInit(){
      this.userRole = this.storage.getRole();
      this.getActiveCustomer();
      console.log(this.userRole)

  
    }// End-ngOnInit

    getActiveCustomer(){
      return this.customerService.getCustomer()
      .subscribe((response : Customer[]) => {
        this.customer = response.filter(customer => customer.isActive === true);
      })
    }// End-getProducts

    addCustomer(name:string ,email:string , phone:string , role:string , password:string , profit:number){
      this.customerService.addCustomer(name,email,phone,role,password,profit).subscribe();
      window.location.reload();
    }// End-addProduct

    updateCustomer(customer:Customer){

      if(confirm("Are you sure to update ?")){  
        this.customerService.updateCustomer(customer).subscribe();
      }
    } // End-updateProduct

    deleteCustomer(id:Customer){
      if(confirm("Are you sure to delete ?")){  
        this.customerService.deleteCustomer(id).subscribe();
      }
      window.location.reload();
    }// End-deleteProduct


    getCustomerForModal(customer:Customer){
      this.selectedCustomer = customer;
    } // End-getProductForModal
}
