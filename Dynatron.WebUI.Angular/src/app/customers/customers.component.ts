import { Component, OnInit } from "@angular/core";
import { NgbDropdown, NgbDropdownItem, NgbDropdownMenu, NgbDropdownToggle, NgbModal} from "@ng-bootstrap/ng-bootstrap";
import { CustomerService } from "../services/customer.service";
import { Customer } from "../models/customer.model";
import { DatePipe, NgClass } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { CustomerDetailsComponent } from "./customer-details.component";
import { CustomerModalComponent } from "./customer-modal.component";

@Component({
  selector: "customers",
  templateUrl: "customers.component.html",
  standalone: true,
  imports: [CustomerDetailsComponent, FormsModule, NgClass, NgbDropdown, NgbDropdownToggle, NgbDropdownMenu, NgbDropdownItem, DatePipe]
})
export class CustomersComponent implements OnInit {
  customers: Customer[] = [];
  selectedCustomer!: Customer;
  customerStorageKey: string = 'selected_customer';
  constructor(private customerService: CustomerService, private modalService: NgbModal) { }

  ngOnInit() {
    // Get Customers
    this.customerService.getCustomers().subscribe(customers => {
      this.customers = customers;

      // Load selected customer from session storage
      let customerSessionStorageValue = sessionStorage.getItem(this.customerStorageKey);
      if (customerSessionStorageValue) {
        this.selectedCustomer = JSON.parse(customerSessionStorageValue);
      }
      
      if (!this.selectedCustomer && customers.length > 0) {
        // set the first customer as selected
        this.selectCustomer(customers[0]);
      }
    });
  }

  selectCustomer(customer: Customer) {
    this.selectedCustomer = customer;
    sessionStorage.setItem(this.customerStorageKey, JSON.stringify(customer.customerId));
  }

  deleteCustomer(customerId: number) {
    this.customerService.deleteCustomer(customerId).subscribe(() => {
      this.customers = this.customers.filter(c => c.customerId !== customerId);
    });
  }

  openModal(isEditMode: boolean, customer?: Customer) {
    const modalRef = this.modalService.open(CustomerModalComponent, { size: 'md' });
    modalRef.componentInstance.isEditMode = isEditMode;
    modalRef.componentInstance.customer = customer ? { ...customer } : new Customer(0, '', '', '');

    // Set selected customer in edit mode
    if (isEditMode && customer) {
      this.selectCustomer(customer);
    }

    modalRef.result.then((savedCustomer: Customer) => {
      // Success result from modal
      if (!isEditMode) {
        // Push new customer to customers and set as selected customer
        this.customers.push(savedCustomer);
        this.selectCustomer(savedCustomer);
      } else {
        // Update
        const index = this.customers.findIndex(c => c.customerId === savedCustomer.customerId);
        if (index !== -1) {
          this.customers[index] = savedCustomer;
        }
      }
      console.log(savedCustomer);
    }, (reason) => {
      // Handle dismiss
      //console.log(reason);
    });
  }
}
