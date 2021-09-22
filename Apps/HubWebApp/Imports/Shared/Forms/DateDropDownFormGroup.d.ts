import { BlockViewModel } from "../Html/BlockViewModel";
import { DateConstraintCollection } from "./ConstraintCollection";
import { DropDownFormGroup } from "./DropDownFormGroup";
import { ErrorList } from "./ErrorList";
export declare class DateDropDownFormGroup extends DropDownFormGroup<Date> {
    constructor(prefix: string, name: string, vm?: BlockViewModel);
    readonly constraints: DateConstraintCollection;
    protected validateConstraints(fieldErrors: ErrorList): void;
}
