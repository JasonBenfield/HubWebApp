import { BlockViewModel } from "../Html/BlockViewModel";
import { Select } from "../Html/Select";
import { SelectOption } from "../Html/SelectOption";
import { ConstraintCollection } from "./ConstraintCollection";
import { ErrorList } from "./ErrorList";
import { SimpleFieldFormGroup } from "./SimpleFieldFormGroup";
export declare class DropDownFormGroup<TValue> extends SimpleFieldFormGroup<TValue> {
    constructor(prefix: string, name: string, vm?: BlockViewModel);
    readonly constraints: ConstraintCollection;
    protected validateConstraints(fieldErrors: ErrorList): void;
    readonly select: Select<TValue>;
    readonly valueChanged: import("../Events").DefaultEventHandler<TValue>;
    getValue(): TValue;
    setValue(value: TValue): void;
    setItems(...items: SelectOption<TValue>[]): void;
    setItemCaption(itemCaption: string): void;
}
