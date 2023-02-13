export enum UserStatuses{
  Active,
  Suspended,
  Deleted
}

export const UserStatusesLabel = new Map<number, string>([
  [UserStatuses.Active, 'AKTİF'],
  [UserStatuses.Suspended, 'ASKIYA ALINDI'],
  [UserStatuses.Deleted, 'SİLİNDİ']
]);
