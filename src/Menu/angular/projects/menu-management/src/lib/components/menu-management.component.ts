import { Component, OnInit } from '@angular/core';
import { MenuManagementService } from '../services/menu-management.service';

@Component({
  selector: 'lib-menu-management',
  template: ` <p>menu-management works!</p> `,
  styles: [],
})
export class MenuManagementComponent implements OnInit {
  constructor(private service: MenuManagementService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
