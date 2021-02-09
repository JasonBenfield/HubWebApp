import { BlockViewModel } from "../Html/BlockViewModel";
import { NumberConstraintCollection } from "./ConstraintCollection";
import { DropDownFormGroup } from "./DropDownFormGroup";
import { ErrorList } from "./ErrorList";
export declare class NumberDropDownFormGroup extends DropDownFormGroup<number> {
    constructor(prefix: string, name: string, vm?: BlockViewModel);
    readonly constraints: NumberConstraintCollection;
    protected validateConstraints(fieldErrors: ErrorList): void;
}
