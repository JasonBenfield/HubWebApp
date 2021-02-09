import { HtmlComponent } from "./HtmlComponent";
import { ContextualClass } from "../ContextualClass";
import { SelectViewModel } from "./SelectViewModel";
import { SelectOption } from "./SelectOption";
export declare class Select<T> extends HtmlComponent {
    constructor(vm?: SelectViewModel);
    protected readonly vm: SelectViewModel;
    enable(): void;
    disable(): void;
    private border;
    setBorder(border: ContextualClass): void;
    private getBorderCss;
    setItemCaption(itemCaption: string): void;
    setItems(items: SelectOption<T>[]): void;
    getValue(): T;
    setValue(value: T): void;
}
