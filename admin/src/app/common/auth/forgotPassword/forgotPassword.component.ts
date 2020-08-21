import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { fadeInUp400ms } from '../../../../@vex/animations/fade-in-up.animation';
import { AuthService } from 'src/app/_service/auth/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgotPassword.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  animations: [
    fadeInUp400ms
  ]
})
export class ForgotPasswordComponent implements OnInit {

  form: FormGroup;

  isSent: boolean;

  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private cd: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      email: ['', Validators.required]
    });

    this.isSent = false;
  }

  send() {

    if (this.form.invalid) {
      return;
    }

    const email = this.form.value.email as string;
    this.authService.forgotPassword( email ).subscribe(
      () => { 
        this.isSent = true; 
        this.cd.markForCheck();
      }
    );
  }

}
 