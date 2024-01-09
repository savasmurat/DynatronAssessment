import { Component, OnDestroy, OnInit } from "@angular/core";
import { ErrorHandlerInterceptor } from "../interceptors/error-handler-interceptor";

@Component({
  selector: "errors",
  templateUrl: "errors.component.html",
  standalone: true
})
export class ErrorsComponent implements OnInit {
  private lastErrors: string[] = [];

  constructor(private errorService: ErrorHandlerInterceptor) {

  }

  ngOnInit() {
    this.errorService.errors.subscribe(errors => {
      this.lastErrors = errors;
    });
  }

  get errors(): string[] {
    return this.lastErrors;
  }
}
