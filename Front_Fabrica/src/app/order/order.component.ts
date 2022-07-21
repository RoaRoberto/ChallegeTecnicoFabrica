import { Component, OnInit } from '@angular/core';
import { MessageToast } from '../dto/MessageToast';
import { OrderDto } from '../dto/OrderDto';
import { ProductDto } from '../dto/ProductDto';
import { UserDto } from '../dto/UserDto';
import { OrderService } from '../services/Order.service';
import { ProductService } from '../services/Product.service';
import { UserService } from '../services/User.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],
})
export class OrderComponent implements OnInit {
  Orders: OrderDto[] = [];
  Auth: OrderDto[] = [];
  regModel: OrderDto = {} as OrderDto;
  showNew: Boolean = false;
  submitType: string = 'Save';
  selectedRow: number = 0;
  message: MessageToast = {} as MessageToast;
  show: boolean = false;
  users: UserDto[] = [];
  products: ProductDto[] = [];
  productSelected: string = '';
  userSelected: string = '';
  toStr = JSON.stringify;

  constructor(
    private orderservice: OrderService,
    private userService: UserService,
    private productService: ProductService
  ) {}

  async ngOnInit() {
    await this.getData();
    this.users = await this.userService.getAllAsync();
    this.products = await this.productService.getAllAsync();
  }

  async getData() {
    this.Orders = await this.orderservice.getAllAsync();
  }

  onNew() {
    this.submitType = 'Save';
    this.userSelected = '';
    this.productSelected = '';

    this.regModel = {
      id: 0,
      amount: 0,
      iva: 0,
      productId: 0,
      userId: 0,
      subtotal: 0,
      total: 0,
      unitvalue: 0,
    } as OrderDto;
    this.showNew = true;
  }

  async onSave() {
    if (this.submitType === 'Save') {
      try {
        const response = await this.orderservice.createAsync(this.regModel);
        console.log(response);
        this.getData();
        this.showNew = false;
        this.regModel = {} as OrderDto;
        this.showAlert('Success', 'Success');
      } catch (error: any) {
        console.log(error);
        this.showAlert('Error', error.error);
      }
    } else {
      try {
        const response = await this.orderservice.updateAsync(
          this.regModel,
          this.regModel.id + ''
        );

        this.getData();
        this.showNew = false;
        this.regModel = {} as OrderDto;
        this.showAlert('Success', 'Success');
      } catch (error: any) {
        console.log(error);
        this.showAlert('Error', error.error);
      }
    }
  }

  onEdit(id: number) {
    this.selectedRow = id;
    this.regModel = {} as OrderDto;
    this.regModel = Object.assign(
      {},
      this.Orders.find((i) => i.id === this.selectedRow)
    );
    const product = this.products.find((i) => i.id === this.regModel.productId);
    const user = this.users.find((i) => i.id === this.regModel.userId);

    this.userSelected = this.toStr(user);
    this.productSelected = this.toStr(product);

    this.submitType = 'Update';
    this.showNew = true;
  }

  async onDelete(id: number) {
    try {
      const response = await this.orderservice.deleteAsync(id + '');
      this.getData();
      this.showAlert('Success', 'Success');
    } catch (error: any) {
      this.showAlert('Error', error.error);
    }
  }

  onCancel() {
    this.showNew = false;
  }

  showAlert(title: string, body: string) {
    if (title == 'Error') {
      this.message.class = 'bg-success text-light';
    } else if (title == 'Exito') {
      this.message.class = 'bg-danger text-light';
    } else {
      this.message.class = 'text-light';
    }
    this.message.body = body;
    this.message.title = title;
    this.show = true;
    setTimeout(() => (this.show = false), 5000);
  }

  onChangeProduct(value: string) {
    const product = JSON.parse(value) as ProductDto;
    this.regModel.unitvalue = product.value;
    this.regModel.productId = product.id;
    this.calcular();
  }

  onChangePUsert(value: string) {
    const user = JSON.parse(value) as UserDto;
    this.regModel.userId = user.id;
  }

  amountchange(value: number) {
    this.calcular();
  }
  calcular() {
    this.regModel.subtotal = this.regModel.amount * this.regModel.unitvalue;
    this.regModel.iva = this.regModel.subtotal * (19 / 100);
    this.regModel.total = this.regModel.subtotal + this.regModel.iva;
  }
}
