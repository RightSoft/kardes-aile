export enum Genders {
  Unknown = 0,
  Male = 1,
  Female = 2
}

export const GendersLabel = new Map<number, string>([
  [Genders.Unknown, 'Bilinmiyor'],
  [Genders.Male, 'Erkek'],
  [Genders.Female, 'Kız']
]);
