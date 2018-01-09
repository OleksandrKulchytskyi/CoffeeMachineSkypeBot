import { AlertService } from './alert.service';
import { LocalStorageService } from './local-storage.service';

export const services: any[] = [ AlertService, LocalStorageService ];

export * from './alert.service';
export * from './local-storage.service';
