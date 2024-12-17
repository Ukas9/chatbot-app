import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-add',
  templateUrl: './user-add.component.html',
  styleUrl: './user-add.component.css'
})
export class UserAddComponent {
  @Output() userAdd = new EventEmitter<string>();

  public userForm: FormGroup;

  constructor(
  ) {
    this.userForm = new FormGroup({
      userName: new FormControl('', [Validators.required]) // Walidacja 'required'
    });
  }

  // Funkcja do wyświetlania wartości formularza
  onSubmit() {
    if(!this.userForm.valid) {
      return;
    }

      console.log('Wartość formularza:', this.userForm.value);
    this.userAdd.emit(this.userForm.value);
  }
}
