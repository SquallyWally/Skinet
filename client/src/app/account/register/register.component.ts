import { Component } from '@angular/core';
import {
  AbstractControl,
  AsyncValidatorFn,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { debounceTime, finalize, map, switchMap, take } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  errors: string[] | null = null;
  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router
  ) {}

  complexPassword = '^(?=[^\\d_].*?\\d)\\w(\\w|[!@#$%]){7,20}';

  registerForm = this.formBuilder.group({
    displayName: ['', Validators.required],
    email: [
      '',
      [Validators.required, Validators.email],
      [this.validateEmailNotTaken()],
    ],
    password: [
      '',
      [Validators.required, Validators.pattern(this.complexPassword)],
    ],
  });

  onSubmit() {
    this.accountService.register(this.registerForm.value).subscribe({
      next: () => this.router.navigateByUrl('/shop'),
      error: (error) => (this.errors = error.errors),
    });
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return (control: AbstractControl) => {
      return control.valueChanges.pipe(
        debounceTime(1000), // debounce from api
        take(1), // only need the latest observable
        switchMap(() => { //projects the value to an observable
          return this.accountService.checkEmailExists(control.value).pipe(
            map((result) => (result ? { emailExists: true } : null)),
            finalize(() => control.markAsTouched())
          ); // transform into an validation result
        })
      );
    };
  }
}
