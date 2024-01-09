export class Customer {
  public createdDateTime!: Date;
  public lastUpdatedDateTime!: Date;
  constructor(
    public customerId: number = 0,
    public firstName: string = '',
    public lastName: string = '',
    public email: string = ''
  ) {
  }
}
