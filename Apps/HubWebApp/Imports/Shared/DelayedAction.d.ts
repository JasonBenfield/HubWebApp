export declare class DelayedAction<TResult> {
    private readonly func;
    private readonly wait;
    static delay(wait: number): Promise<void>;
    constructor(func: (...args: any[]) => TResult, wait: number);
    execute(...args: any[]): Promise<TResult>;
}
