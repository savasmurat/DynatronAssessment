import { Component, Input } from "@angular/core";
import { Customer } from "../models/customer.model";
import { FormsModule } from "@angular/forms";

@Component({
  selector: "customer-details",
  templateUrl: "customer-details.component.html",
  standalone: true,
  imports: [FormsModule]
})
export class CustomerDetailsComponent {
  @Input() customer!: Customer;
  constructor() {

  }
}
