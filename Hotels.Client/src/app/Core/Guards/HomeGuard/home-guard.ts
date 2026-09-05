import { isPlatformBrowser } from '@angular/common';
import { inject, PLATFORM_ID } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const homeGuard: CanActivateFn = (route, state) => {
  const platformKey = inject(PLATFORM_ID);
  const router = inject(Router);
  const token = isPlatformBrowser(platformKey) ? localStorage.getItem("token") : ""
  if(token == null || token == "") {
    router.navigate(["/Login"])
    return false;
  }
  return true;
};
