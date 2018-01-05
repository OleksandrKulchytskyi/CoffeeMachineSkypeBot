import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { UserService } from '../../services/';
import { User } from '../../models/user';

@Component({
    selector: 'user-view',
    styleUrls: ['user-view.component.scss'],
    templateUrl: 'user-view.component.html',
  })

  export class UserViewComponent implements OnInit {
    currentUser: User;

    constructor(private userService: UserService,
                private route: ActivatedRoute,
                private router: Router ) {
    }

    ngOnInit() {
    }

    onSelect(event: number[]) {
      }
    
      onCreate(event: User) {
      }
    
      onUpdate(event: User) {
        // this.pizzaService.updatePizza(event).subscribe(() => {
        //   this.router.navigate([`/products`]);
        // });
      }
    
      onRemove(event: User) {
        const remove = window.confirm('Are you sure?');
        if (remove) {
        }
      }
}