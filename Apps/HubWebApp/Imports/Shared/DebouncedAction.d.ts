export declare class DebouncedAction<TResult> {
    private readonly debounced;
    constructor(func: (...args: any) => TResult, wait?: number);
    execute(...args: any[]): TResult;
}
