export declare class CssClass {
    constructor(...initialNames: string[]);
    private value;
    private readonly names;
    clear(): this;
    addFrom(cssClass: CssClass): this;
    addName(name: string): this;
    addNames(...names: string[]): this;
    private _addName;
    removeName(name: string): this;
    removeNames(...names: string[]): this;
    private _removeName;
    private updateValue;
    toString(): string;
}
