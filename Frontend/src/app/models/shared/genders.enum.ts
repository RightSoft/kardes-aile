export enum Genders {
  Unknown,
  Male,
  Female
}

export const GendersLabel = new Map<number, string>([
  [Genders.Unknown, 'Bilinmiyor'],
  [Genders.Male, 'Erkek'],
  [Genders.Female, 'Kız']
]);
