import { AlertService } from './alert.service';
import { AuthGuard } from './auth.guard';

export const services: any[] = [ AlertService, AuthGuard ];

export * from './alert.service';
export * from './auth.guard';
