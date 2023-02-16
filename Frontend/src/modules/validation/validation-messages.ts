import {passwordRegEx} from '@appModule/constants/password-regex';

export const validationMessages: { [key: string]: string } = {
  required: 'Zorunlu alan.',
  email: 'Geçerli bir email adresi giriniz.',
  phone: 'Geçerli bir telefon numarası giriniz.',
  url: 'Geçerli bir URL giriniz.',
  confirmPassword: 'Parolalarınız eşleşmiyor.'
};
export const validationMessagesFunctions = {
  minLength: (length: number) => `Can be at least ${length} characters.`,
  maxLength: (length: number) => `Can be up to ${length} characters.`,
  max: (max: number) => `Can be up to ${String(max).length} digit.`,
  min: (min: number) => `Can be at least ${String(min).length} digit.`,
  pattern: (pattern: string) => {
    switch (String(pattern)) {
      case String(passwordRegEx):
        return 'Your password must contain at least one lowercase letter, at least one uppercase letter, and at least one number.';
      default:
        return null;
    }
  }
};
