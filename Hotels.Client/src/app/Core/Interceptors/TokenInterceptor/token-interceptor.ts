import { isPlatformBrowser } from '@angular/common';
import { HttpInterceptorFn } from '@angular/common/http';
import { inject, PLATFORM_ID } from '@angular/core';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  if (!req.url.includes("Login") && !req.url.includes("Register")) {
    const platformKey = inject(PLATFORM_ID);
    const token = isPlatformBrowser(platformKey) ? localStorage.getItem("token") : "";
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    })
  }
  return next(req);
};
