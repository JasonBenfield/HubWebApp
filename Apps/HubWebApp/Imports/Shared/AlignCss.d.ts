import { CssClass } from "./CssClass";
declare type AlignType = 'auto' | 'start' | 'end' | 'center' | 'baseline' | 'stretch';
declare type AlignContentType = 'start' | 'end' | 'center' | 'between' | 'around' | 'stretch';
export declare class AlignCssType {
    private readonly type;
    private readonly breakpoints;
    constructor(type: string);
    xs(alignType: AlignType): void;
    sm(alignType: AlignType): void;
    md(alignType: AlignType): void;
    lg(alignType: AlignType): void;
    xl(alignType: AlignType): void;
    xxl(alignType: AlignType): void;
    cssClass(): CssClass;
    private getCssName;
    toString(): string;
}
export declare class AlignContentCssType {
    private readonly breakpoints;
    constructor();
    xs(alignType: AlignContentType): void;
    sm(alignType: AlignContentType): void;
    md(alignType: AlignContentType): void;
    lg(alignType: AlignContentType): void;
    xl(alignType: AlignContentType): void;
    xxl(alignType: AlignContentType): void;
    cssClass(): CssClass;
    private getCssName;
    toString(): string;
}
export declare class AlignCss {
    private readonly types;
    items(config: (item: AlignCssType) => void): this;
    content(config: (item: AlignContentCssType) => void): this;
    self(config: (item: AlignCssType) => void): this;
    cssClass(): CssClass;
    toString(): string;
}
export {};
