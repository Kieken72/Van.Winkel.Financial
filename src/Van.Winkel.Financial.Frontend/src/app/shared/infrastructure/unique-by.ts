export function uniqueBy<T>(key: keyof T) {
    return (value: T, index: number, self: T[]) =>
        self.findIndex(s => s[key] === value[key]) === index;
}
