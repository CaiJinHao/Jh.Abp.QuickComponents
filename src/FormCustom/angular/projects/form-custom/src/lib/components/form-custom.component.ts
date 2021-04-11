import { Component, OnInit } from '@angular/core';
import { FormCustomService } from '../services/form-custom.service';

@Component({
  selector: 'lib-form-custom',
  template: ` <p>form-custom works!</p> `,
  styles: [],
})
export class FormCustomComponent implements OnInit {
  constructor(private service: FormCustomService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
