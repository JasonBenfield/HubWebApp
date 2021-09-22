import { ContextualClass } from "./ContextualClass";
import { CssClass } from "./CssClass";
export declare class TextCss {
    private aligns;
    start(): this;
    end(): this;
    center(): this;
    private _color;
    context(context: ContextualClass): this;
    muted(): this;
    resetColor(): this;
    private _truncate;
    truncate(): this;
    private fontWeight;
    bold(): this;
    bolder(): this;
    private size;
    fontSize(size: 1 | 2 | 3 | 4 | 5): void;
    private style;
    italicize(): void;
    cssClass(): CssClass;
    toString(): string;
}
