import { Injectable } from '@angular/core';

@Injectable()
export class LocalStorageService {
  public isSupported = false;

  private prefix = 'ma-ls';
  private customPrefix = '';

  constructor() {
    this.isSupported = this.checkSupport();
  }

  set(key, data): void {
    if (this.isSupported) {
      localStorage.setItem(this.deriveKey(key), JSON.stringify(data));
    }
  }

  get(key): any {
    if (this.isSupported) {
      const data = JSON.parse(localStorage.getItem(this.deriveKey(key)));
      return data || null;
    } else {
      return null;
    }
  }

  setPrefix(value): void {
    this.customPrefix = value;
  }

  private deriveKey(key: string): string {
    return this.prefix + (this.customPrefix ? '.' + this.customPrefix : '') + '__' + key;
  }

  private checkSupport(): boolean {
    try {
      const supported = 'localStorage' in window && window.localStorage !== null;

      if (supported) {
        // When Safari (OS X or iOS) is in private browsing mode, it
        // appears as though localStorage is available, but trying to
        // call .setItem throws an exception.
        //
        // "QUOTA_EXCEEDED_ERR: DOM Exception 22: An attempt was made
        // to add something to storage that exceeded the quota."
        const storage = window.localStorage;
        const key = this.deriveKey(`${Math.round(Math.random() * 1e7)}`);
        storage.setItem(key, '');
        storage.removeItem(key);
      }

      return supported;
    } catch (e) {
      return false;
    }
  }
}
