import { Component } from '@angular/core';
import { Customer } from '../../customer/customer';
import { CustomerService } from 'src/app/services/customer.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-user-informations',
  templateUrl: './user-informations.component.html',
  styleUrls: ['./user-informations.component.css']
})
export class UserInformationsComponent {

  customer!: Customer;
  userRole?: string;
  userId?: string

  constructor(
    private storage: StorageService,
    private customerService: CustomerService,
  ) { }

  ngOnInit() {
    this.userRole = this.storage.getRole();
    this.userId = this.storage.getCustomerId();
    this.getCustomerById();

  }// End-ngOnInit

  getCustomerById() {
    this.customerService.getCustomerById(this.userId!)
      .subscribe(response =>
        this.customer = response
      );
  }

  updateCustomer(customer: Customer) {
    if (confirm("Are you sure to update ?")) {
      this.customerService.updateCustomer(customer).subscribe();
    }
  } // End-updateProduct

}
