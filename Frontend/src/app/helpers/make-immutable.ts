const makeImmutable = <T>(obj: T): T => {
  if (obj) {
    return JSON.parse(JSON.stringify(obj)) as T;
  }
  return obj;
};
export default makeImmutable;
