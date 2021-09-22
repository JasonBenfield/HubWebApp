import { CssClass } from "./CssClass";
export declare type PaddingAmount = 0 | 1 | 2 | 3 | 4 | 5 | 'auto';
export interface PaddingAmounts {
    bottom?: PaddingAmount;
    top?: PaddingAmount;
    start?: PaddingAmount;
    end?: PaddingAmount;
}
export declare class PaddingCss {
    static bottom(amount: PaddingAmount): PaddingCss;
    static top(amount: PaddingAmount): PaddingCss;
    static start(amount: PaddingAmount): PaddingCss;
    static end(amount: PaddingAmount): PaddingCss;
    static xs(amounts: PaddingAmounts | PaddingAmount): PaddingCss;
    static sm(amounts: PaddingAmounts | PaddingAmount): PaddingCss;
    static md(amounts: PaddingAmounts | PaddingAmount): PaddingCss;
    static lg(amounts: PaddingAmounts | PaddingAmount): PaddingCss;
    static xl(amounts: PaddingAmounts | PaddingAmount): PaddingCss;
    static xxl(amounts: PaddingAmounts | PaddingAmount): PaddingCss;
    private readonly css;
    xs(amounts: PaddingAmounts | PaddingAmount): this;
    sm(amounts: PaddingAmounts | PaddingAmount): this;
    md(amounts: PaddingAmounts | PaddingAmount): this;
    lg(amounts: PaddingAmounts | PaddingAmount): this;
    xl(amounts: PaddingAmounts | PaddingAmount): this;
    xxl(amounts: PaddingAmounts | PaddingAmount): this;
    private addCssForBreakpoint;
    private isPaddingAmount;
    private getCss;
    cssClass(): CssClass;
    toString(): string;
}
