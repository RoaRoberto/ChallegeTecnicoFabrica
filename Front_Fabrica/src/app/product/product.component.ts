import { Component, OnInit } from '@angular/core';
import { MessageToast } from '../dto/MessageToast';
import { ProductDto } from '../dto/ProductDto';
import { ProductService } from '../services/Product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  Products: ProductDto[] = [];
  Auth: ProductDto[] = [];
  regModel: ProductDto = {} as ProductDto;
  showNew: Boolean = false;
  submitType: string = 'Save';
  selectedRow: number = 0;
  message: MessageToast = {} as MessageToast;
  show: boolean = false;
  constructor(private productservice: ProductService) {

  }

  async ngOnInit() {
    await this.getData();
  }

  async getData() {
    this.Products = await this.productservice.getAllAsync();
  }

  onNew() {

    this.submitType = 'Save';
    this.regModel ={description:'',id:0,value:0 } as ProductDto ;
    this.showNew = true;
  }

  async onSave() {
    if (this.submitType === 'Save') {

        try {
          const response = await this.productservice.createAsync(this.regModel);
          console.log(response);
          this.getData();
          this.showNew = false;
          this.regModel = {} as ProductDto;
          this.showAlert("Success", "Success");
        } catch (error: any) {
          console.log(error);
          this.showAlert("Error", error.error);
        }


    } else {
      try {
        const response = await this.productservice.updateAsync(this.regModel, this.regModel.id + '');


        this.getData();
        this.showNew = false;
        this.regModel = {} as ProductDto;
        this.showAlert("Success", "Success");

      } catch (error: any) {
        console.log(error);
        this.showAlert("Error", error.error);
      }


    }

  }

  onEdit(id: number) {
    this.selectedRow = id;
    this.regModel = {} as ProductDto;
    this.regModel = Object.assign({}, this.Products.find(i => i.id === this.selectedRow));
    this.submitType = 'Update';
    this.showNew = true;
  }

  async onDelete(id: number) {
    try {
      const response = await this.productservice.deleteAsync(id + '');
      this.getData();
      this.showAlert("Success", "Success");

    } catch (error: any) {

      this.showAlert("Error", error.error);
    }


  }

  onCancel() {
    this.showNew = false;

  }

  showAlert(title: string, body: string) {
    if (title == "Error") {
      this.message.class = 'bg-success text-light';
    } else
      if (title == "Exito") {
        this.message.class = 'bg-danger text-light';
      } else {
        this.message.class = 'text-light';

      }
    this.message.body = body;
    this.message.title = title;
    this.show = true;
    setTimeout(() => this.show = false, 5000);

  }

}
