import { BlockViewModel } from "../Html/BlockViewModel";
import { MessageAlert } from "../MessageAlert";
import { CardBody } from "./CardBody";
export declare class CardAlert extends CardBody {
    constructor(vm?: BlockViewModel);
    readonly alert: MessageAlert;
    private onMessageChanged;
}
