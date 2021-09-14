import { CssClass } from "./CssClass";
export declare class ColumnCssForBreakpoint {
    private readonly breakpoint;
    private readonly size;
    constructor(breakpoint: string, size: ColumnCssSize);
    cssClassName(): string;
    toString(): string;
}
export declare class ColumnCss implements IColumnCss {
    static xs(columnSize?: ColumnCssSize): ColumnCss;
    static sm(columnSize?: ColumnCssSize): void;
    static lg(columnSize?: ColumnCssSize): void;
    static xl(columnSize?: ColumnCssSize): void;
    static xxl(columnSize?: ColumnCssSize): void;
    private constructor();
    private breakpoints;
    xs(columnSize?: ColumnCssSize): this;
    sm(columnSize?: ColumnCssSize): void;
    md(columnSize?: ColumnCssSize): void;
    lg(columnSize?: ColumnCssSize): void;
    xl(columnSize?: ColumnCssSize): void;
    xxl(columnSize?: ColumnCssSize): void;
    cssClass(): CssClass;
    toString(): string;
}
