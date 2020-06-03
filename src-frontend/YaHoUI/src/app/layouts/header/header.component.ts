import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subject, interval } from 'rxjs';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { map, shareReplay, takeUntil } from 'rxjs/operators';
import { IUser, IUserInfo } from 'src/app/core/interfaces/user.interface';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { AuthenticationService } from 'src/app/features/authentication/authentication.service';
import { AccountService } from 'src/app/features/account/common/account.api.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  currentUser: IUser;
  userInfo: IUserInfo;
  interval: any;

  private destroy$ = new Subject<void>();
  constructor(private router: Router,
              private breakpointObserver: BreakpointObserver,
              public dialog: MatDialog,
              private accountService: AccountService,
              private authenticationService: AuthenticationService) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  ngOnInit(): void {
    this.interval = interval(3000).subscribe(() => this.getUserInfo());
  }

  getUserInfo(): void {
    this.accountService.getMyInfo()
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        res => this.userInfo = res,
        () => {
          clearInterval(this.interval);
        }
      );
  }


  userAuthenticated(): boolean {
    return this.currentUser !== null;
  }

  login(): void {
    this.router.navigate(['/auth/sign-in']);
  }

  registration(): void {
    this.router.navigate(['/auth/sign-up']);
  }

  logout(): void {
    this.authenticationService.logout();
    this.login();
  }

  ngOnDestroy(): void {
    clearInterval(this.interval);
    this.destroy$.next();
    this.destroy$.complete();
  }

}
