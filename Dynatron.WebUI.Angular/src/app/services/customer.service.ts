import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Customer } from "../models/customer.model";
import { Observable } from "rxjs";


const customersUrl: string = `${environment.apiUrl}/customers`;

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  constructor(private http: HttpClient) { }

  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(customersUrl);
  }

  createCustomer(customer: Customer) {
    return this.http.post<Customer>(customersUrl, customer);
  }

  updateCustomer(customer: Customer) {
    return this.http.put<Customer>(`${customersUrl}/${customer.customerId}`, customer);
  }

  deleteCustomer(customerId: number) {
    return this.http.delete(`${customersUrl}/${customerId}`);
  }
}
