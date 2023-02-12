import { Injectable, NgZone } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {
  private snackBarSuccessStyle = 'snackbar-success';
  private snackBarErrorStyle = 'snackbar-error';

  constructor(
    private readonly snackBar: MatSnackBar,
    private readonly zone: NgZone
  ) {}

  show(title: string, text: string) {
    const panelClass = 'success'
      ? this.snackBarSuccessStyle
      : this.snackBarErrorStyle;

    this.zone.run(() => {
      const snackBar = this.snackBar.open(text, null, {
        panelClass: panelClass,
        verticalPosition: 'bottom',
        horizontalPosition: 'center',
        duration: 3000
      });

      snackBar.onAction().subscribe(() => {
        snackBar.dismiss();
      });
    });
  }
}
