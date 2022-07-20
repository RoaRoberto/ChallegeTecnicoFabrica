import { Component, OnInit } from '@angular/core';
import { MessageToast } from '../dto/MessageToast';
import { UserDto } from '../dto/UserDto';
import { UserService } from '../services/User.service';


@Component({
  selector: 'app-User',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  Users: UserDto[] = [];
  Auth: UserDto[] = [];
  regModel: UserDto = {} as UserDto;
  showNew: Boolean = false;
  submitType: string = 'Save';
  selectedRow: number = 0;
  message: MessageToast = {} as MessageToast;
  show: boolean = false;
  constructor(private Userservice: UserService) {

  }

  async ngOnInit() {
    await this.getData();
  }

  async getData() {
    this.Users = await this.Userservice.getAllAsync();
  }

  onNew() {

    this.submitType = 'Save';
    this.regModel ={ name: "", id: 0,login:"",email:"",password:"" } as UserDto ;
    this.showNew = true;
  }

  async onSave() {
    if (this.submitType === 'Save') {

        try {
          const response = await this.Userservice.createAsync(this.regModel);
          console.log(response);
          this.getData();
          this.showNew = false;
          this.regModel = {} as UserDto;
          this.showAlert("Success", "Success");
        } catch (error: any) {
          console.log(error);
          this.showAlert("Error", error.error);
        }


    } else {
      try {
        const response = await this.Userservice.updateAsync(this.regModel, this.regModel.id + '');


        this.getData();
        this.showNew = false;
        this.regModel = {} as UserDto;
        this.showAlert("Success", "Success");

      } catch (error: any) {
        console.log(error);
        this.showAlert("Error", error.error);
      }


    }

  }

  onEdit(id: number) {
    this.selectedRow = id;
    this.regModel = {} as UserDto;
    this.regModel = Object.assign({}, this.Users.find(i => i.id === this.selectedRow));
    this.submitType = 'Update';
    this.showNew = true;
  }

  async onDelete(id: number) {
    try {
      const response = await this.Userservice.deleteAsync(id + '');
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
