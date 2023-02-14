export enum UserStatuses {
  Active,
  Suspended,
  Deleted
}

export const UserStatusesLabel = new Map<UserStatuses, string>([
  [UserStatuses.Active, 'AKTİF'],
  [UserStatuses.Suspended, 'ASKIYA ALINDI'],
  [UserStatuses.Deleted, 'SİLİNDİ']
]);
