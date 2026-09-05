import { isPlatformBrowser } from '@angular/common';
import { inject, PLATFORM_ID } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const platformKey = inject(PLATFORM_ID);
  const _Router = inject(Router);
  const token = isPlatformBrowser(platformKey) ? localStorage.getItem("token") : "";
  if(token != null && token != "") {
    _Router.navigate(["/Rooms"])
    return false;
  }
  return true;
};
