import { CssClass } from "./CssClass";
export declare type MarginAmount = 0 | 1 | 2 | 3 | 4 | 5 | 'auto';
export interface MarginAmounts {
    bottom?: MarginAmount;
    top?: MarginAmount;
    start?: MarginAmount;
    end?: MarginAmount;
}
export declare class MarginCss {
    static bottom(amount: MarginAmount): MarginCss;
    static top(amount: MarginAmount): MarginCss;
    static start(amount: MarginAmount): MarginCss;
    static end(amount: MarginAmount): MarginCss;
    static xs(amounts: MarginAmounts | MarginAmount): MarginCss;
    static sm(amounts: MarginAmounts | MarginAmount): MarginCss;
    static md(amounts: MarginAmounts | MarginAmount): MarginCss;
    static lg(amounts: MarginAmounts | MarginAmount): MarginCss;
    static xl(amounts: MarginAmounts | MarginAmount): MarginCss;
    static xxl(amounts: MarginAmounts | MarginAmount): MarginCss;
    private readonly css;
    xs(amounts: MarginAmounts | MarginAmount): this;
    sm(amounts: MarginAmounts | MarginAmount): this;
    md(amounts: MarginAmounts | MarginAmount): this;
    lg(amounts: MarginAmounts | MarginAmount): this;
    xl(amounts: MarginAmounts | MarginAmount): this;
    xxl(amounts: MarginAmounts | MarginAmount): this;
    private addCssForBreakpoint;
    private isMarginAmount;
    private getCss;
    cssClass(): CssClass;
    toString(): string;
}
