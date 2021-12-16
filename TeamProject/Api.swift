//
//  Api.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 07/12/2021.
//

import Foundation

struct API {
    struct GET {
        static let listOfBudgets = "https://webapi-nn.herokuapp.com/api/budget"
        static let listOfBudgetsType = "https://webapi-nn.herokuapp.com/api/budget/"
        static let budgetDescription = "https://webapi-nn.herokuapp.com/api/budget/type/"
        static let budgetTypeDescription = "https://webapi-nn.herokuapp.com/api/budgetType/"
    }
    
    struct POST {
        static let addBudget = "https://webapi-nn.herokuapp.com/api/budget/add/"
        static let availableCash = "https://webapi-nn.herokuapp.com/api/availableCash"
        static let unavailableTypeCash = "https://webapi-nn.herokuapp.com/api/unavailableTypeCash"
    }
	
	struct PUT {
		static let getPredictionByType = "https://webapi-nn.herokuapp.com/api/prediction/"
	}
    
    struct DELETE {
        static let deleteBudgetById = "https://webapi-nn.herokuapp.com/api/budget/"
    }
}
