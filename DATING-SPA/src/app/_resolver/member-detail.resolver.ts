import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';

@Injectable()
export class MemberDetailResolver implements Resolve<User> {

    constructor(private userService: UserService, private alertify: AlertifyService, private router: Router) {}

    resolve(route: ActivatedRouteSnapshot): Observable<User> | Promise<User> | User {

        return this.userService.getUser(route.params.id).pipe(
            catchError(error => {
                this.alertify.error('Problem Retrieving Data.');
                this.router.navigate(['/members']);
                return of(null);
            })
        );
    }
}
