import { AlertService } from './alert.service';
import { AuthService } from './auth.service';
import { AuthGuard } from './auth.guard';

export const services: any[] = [ AlertService, AuthService, AuthGuard ];

export * from './alert.service';
export * from './auth.guard';
export * from './auth.service';
