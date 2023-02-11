import { AppService } from './../business/app.service';
import { inject } from '@angular/core';
abstract class AddPageTitle {
  private appService = inject(AppService);
  constructor(pageTitle: string) {
    this.appService.pageTitle$.next(pageTitle);
  }
}
export default AddPageTitle;
