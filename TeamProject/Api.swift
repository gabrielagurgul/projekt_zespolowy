//
//  Api.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 07/12/2021.
//

import Foundation

struct API {
    struct GET {
        static let listOfBudgets = URL(string: "https://webapi-nn.herokuapp.com/api/budget")!
        static let listOfBudgetsType = URL(string: "https://webapi-nn.herokuapp.com/api/budget/")!
        static let budgetDescription = URL(string: "https://webapi-nn.herokuapp.com/api/budget/type/")!
        static let budgetTypeDescription = URL(string: "https://webapi-nn.herokuapp.com/api/budgetType/")!
    }
    
    struct POST {
        static let addBudget = URL(string: "https://webapi-nn.herokuapp.com/api/budget/add/")!
        static let availableCash = URL(string: "https://webapi-nn.herokuapp.com/api/availableCash")!
        static let unavailableTypeCash = URL(string: "https://webapi-nn.herokuapp.com/api/unavailableTypeCash")!
        static let getPredictionByType = URL(string: "https://webapi-nn.herokuapp.com/api/prediction/")!
    }
    
    struct DELETE {
        static let deleteBudgetById = URL(string: "https://webapi-nn.herokuapp.com/api/budget/")!
    }
}
