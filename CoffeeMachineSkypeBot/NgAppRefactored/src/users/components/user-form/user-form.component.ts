import { Component, Input, Output, EventEmitter, OnChanges,
SimpleChanges, ChangeDetectionStrategy} from '@angular/core';

import { FormControl, FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { User } from '../../models';

@Component({
    selector: 'user-form',
    styleUrls: ['user-form.component.scss'],
    templateUrl: 'user-form.component.html'
})

export class UserFormComponent implements OnChanges {
    exists: boolean;

    @Input() user: User;
    @Output() create = new EventEmitter<User>();
    @Output() update = new EventEmitter<User>();
    @Output() remove = new EventEmitter<User>();

  form = this.fb.group({
    username: ['', Validators.required],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
  });

  constructor(private fb: FormBuilder) {
    this.exists = false;
  }

  get usernameControl() {
    return this.form.get('username') as FormControl;
  }

  get usernameControlInvalid() {
    return this.usernameControl.hasError('required') && this.usernameControl.touched;
  }

  get firstNameControl() {
    return this.form.get('firstName') as FormControl;
  }

  get firstNameControlInvalid() {
    return this.firstNameControl.hasError('required') && this.firstNameControl.touched;
  }

  get lastNameControl() {
    return this.form.get('lastName') as FormControl;
  }

  get lastNameControlInvalid() {
    return this.lastNameControl.hasError('required') && this.lastNameControl.touched;
  }

  ngOnChanges(changes: SimpleChanges) {
    if (this.user && this.user.id) {
      this.exists = true;
      this.form.patchValue(this.user);
    }
}

  createUser(form: FormGroup) {
    const { value, valid } = form;
    if (valid) {
      this.create.emit(value);
    }
  }

  updateUser(form: FormGroup) {
    const { value, valid, touched } = form;
    if (touched && valid) {
      this.update.emit({ ...this.user, ...value });
    }
  }

  removeUser(form: FormGroup) {
    const { value } = form;
    this.remove.emit({ ...this.user, ...value });
  }

}
