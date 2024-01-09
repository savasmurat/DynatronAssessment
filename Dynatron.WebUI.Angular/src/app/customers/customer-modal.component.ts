import { Component, Input } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { ErrorsComponent } from "../structure/errors.component";
import { Customer } from "../models/customer.model";
import { CustomerService } from "../services/customer.service";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: 'customer-modal',
  templateUrl: './customer-modal.component.html',
  standalone: true,
  imports: [FormsModule, ErrorsComponent]
})
export class CustomerModalComponent {
  @Input() customer: Customer = new Customer(0, '', '', '');
  @Input() isEditMode: boolean = false;

  constructor(private customerService: CustomerService, public activeModal: NgbActiveModal) {

  }

  get modalTitle() {
    return this.isEditMode ? 'Edit Customer' : 'Create Customer';
  }

  saveChanges() {
    if (!this.isEditMode) {
      this.createCustomer(this.customer);
    } else {
      this.updateCustomer(this.customer);
    }
  }

  createCustomer(model: Customer) {
    this.customerService.createCustomer(model).subscribe(
      newCustomer => {
        this.activeModal.close(newCustomer); //close modal
      }
    );
  }

  updateCustomer(model: Customer) {
    this.customerService.updateCustomer(model).subscribe(updatedCustomer => {
      this.activeModal.close(updatedCustomer); //close modal      
    });
  }

  cancel() {
    this.activeModal.dismiss('Cancelled');
  }
}
