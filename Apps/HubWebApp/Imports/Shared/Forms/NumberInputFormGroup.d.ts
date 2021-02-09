import { BlockViewModel } from "../Html/BlockViewModel";
import { NumberConstraintCollection } from "./ConstraintCollection";
import { ErrorList } from "./ErrorList";
import { InputFormGroup } from "./InputFormGroup";
export declare class NumberInputFormGroup extends InputFormGroup<number> {
    constructor(prefix: string, name: string, vm?: BlockViewModel);
    readonly constraints: NumberConstraintCollection;
    protected validateConstraints(fieldErrors: ErrorList): void;
}
